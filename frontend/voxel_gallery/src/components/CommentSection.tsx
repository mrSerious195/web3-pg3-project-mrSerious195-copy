import { useEffect, useRef, useState } from 'react'
import { useFormik } from "formik";
import type { CommentRequestContract } from '../api/ApiRequest/CommentRequestContract';
import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer';
import MyComment from './MyComment';
import type { CommentResponseContract } from '../api/ApiResponses/CommentResponseContract';
import SubmitButton from './SubmitButton';
import * as Yup from 'yup';
import LineHeader from './LineHeader';
import { VoxelDataApi } from '../api/VoxelDataApi';
interface props{
    sceneID:number
}
const CommentSection = ({sceneID}:props) => {
  const bearer = useHeaderWithBearer()
  const [commends,setCommends] =useState<CommentResponseContract[]>([])
  const newCommentText = useRef<HTMLTextAreaElement>(null);
    const schema = Yup.object().shape({
      comment: Yup.string().required(),
      })
     const { handleChange, handleBlur, handleSubmit, values } = useFormik({
      validationSchema: schema,
        initialValues: {
            comment:""
    
        },
        onSubmit: values => {
            console.log(values)
            const contract:CommentRequestContract ={
              sceneID: sceneID,
              commentText: values.comment
            } 
          
          
            VoxelDataApi.post<CommentResponseContract>("api/Comments",contract,bearer).then(
    ({data})=>{
      values.comment=""
setCommends([...commends, data])
}
            )
        },
      })
    useEffect(()=>{
      const getData =async()=>{ 
        try{
        const data = await VoxelDataApi.get<CommentResponseContract[]>(`api/Comments?sceneID=${sceneID}`,bearer)
        setCommends(data.data?? [])
          console.log("comments");
        console.log(data.data);
        console.log("is array:", Array.isArray(data.data));
         } catch (err){
    console.error("voxel fetch failed: ",err);
  }
    }
    getData()
},[])

  return (
    <div>
       
          <LineHeader>Comments</LineHeader>


    {commends.map((value, idx) => {return  <MyComment key={idx} data={value} 
    SetComments={setCommends } allComments={commends}/>})}
      <form onSubmit={handleSubmit} className="">
         <label htmlFor='comment'>Write a comment</label>
          <textarea ref={newCommentText} id="comment"
            className="flex w-full border  resize-y"
            name="comment"
            value={values.comment}
            onChange={handleChange}
            onBlur={handleBlur}
          />
          <SubmitButton/>
      </form>
    </div>
  )
}

export default CommentSection