import { useFormik } from 'formik';
import * as Yup from 'yup';
import type { CommentRequestContract } from '../api/ApiRequest/CommentRequestContract';
import type { CommentResponseContract } from '../api/ApiResponses/CommentResponseContract';
import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer';
import SubmitButton from './SubmitButton';
import CancelButton from './CancelButton';
import { VoxelDataApi } from '../api/VoxelDataApi';
interface props{
     commentdata: CommentResponseContract
        SetComments:(comments:CommentResponseContract[])=>void
         SetEdditMode:(canEddit:boolean)=>void
        allComments:CommentResponseContract[]
} 

const CommentEdditor = ({commentdata,SetComments,SetEdditMode,allComments}:props) => {
    const bearer =useHeaderWithBearer()

    const schema = Yup.object().shape({
          comment: Yup.string().required(),
          })
         const { handleChange, handleBlur, handleSubmit, values } = useFormik({
          validationSchema: schema,
            initialValues: {
                comment:commentdata.commentText
        
            },
            onSubmit: values => {
                console.log(values)
                const contract:CommentRequestContract ={
                    ...commentdata,
                  commentText: values.comment
                } 
              
              
                VoxelDataApi.put<CommentResponseContract>("api/Comments",contract,bearer).then(
        (value)=>{
            const withoutThis = allComments.filter(c=>c!==commentdata);
            const newCommentList:CommentResponseContract[] =[...withoutThis , value.data];
            const sortedList=newCommentList.sort((a, b) => new Date(a.postTime).getTime() - new Date(b.postTime).getTime())
                   SetComments(sortedList)
                   SetEdditMode(false)
               } )
            },
          })
  return (
      <form onSubmit={handleSubmit} className="">
               <label htmlFor='comment'>Write a comment</label>
                <textarea  id="comment"
                  className="flex w-full border  resize-y"
                  name="comment"
                  value={values.comment}
                  onChange={handleChange}
                  onBlur={handleBlur}
                />
                <div className="flex gap-4">  
                    <CancelButton setCurrentMode={SetEdditMode} />
                    <SubmitButton/>
                </div>
            </form>
  )
}

export default CommentEdditor