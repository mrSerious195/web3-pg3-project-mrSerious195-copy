import { useState } from 'react'
import type { CommentResponseContract } from '../api/ApiResponses/CommentResponseContract'
import { Menu, MenuButton, MenuItem, MenuItems } from '@headlessui/react'

import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer'
import CommentEdditor from './CommentEdditor'
import UserName from './UserName'
import { VoxelDataApi } from '../api/VoxelDataApi'
import { useDarkMode } from '../hooks/useDarkMode'
 interface props{
    data: CommentResponseContract
    SetComments:(comments:CommentResponseContract[])=>void
    allComments:CommentResponseContract[]
}
const MyComment = ({data,SetComments,allComments}:props) => {
  const [isInEdditMode,setEdditMode]=useState(false)
  const bearer = useHeaderWithBearer();
    const date = data && new Date(data.postTime);
  const dateReadable = date && date.toLocaleString("en-GB");



  const deleteThis=async()=>{
    await VoxelDataApi.delete(`api/Comments/${data.commentId}`,bearer).then(
      ()=>{
        const newlist =allComments.filter((obj)=>{return obj!==data})
        SetComments(newlist)
      }
    )
  }
  const donorStatus=data.poster&&data.poster.isDonor
  const {field,highlight}= useDarkMode()
  return (
    <div className={donorStatus?'w-fit  bc bg-green-100 shadow-xl/20':`w-fit  bc ${field} shadow-xl/20`}>
      <div className={donorStatus?' bg-green-300 text-black':highlight}>
       <div className="flex gap-20 justify-between">
         <UserName user={data.poster}/>
            <div>
        <div className="flex items-center">
        <p className="mx-4">{dateReadable}</p>
        {data.poster&&data.poster.isUser?( 
          <Menu as="div" className="relative inline-block">
      <MenuButton className={donorStatus?' bg-green-600 px-2 font-semibold text-white hover:bg-green-500  flex-1':
        " flex-1 bg-white/20 px-2 font-semibold text-white hover:bg-white/40"}>
        X
       
      </MenuButton>

      <MenuItems
        transition
        className={`${highlight} absolute right-0 z-10 mt-2 w-56 origin-top-right 
          outline-1 -outline-offset-1 outline-white/10 transition data-closed:scale-95 
          data-closed:transform data-closed:opacity-0 data-enter:duration-100 data-enter:ease-out data-leave:duration-75 data-leave:ease-in`}
      >
        <div className={donorStatus?' bg-green-300 text-black py-1':"py-1"}>
         
            <MenuItem>
              <button
                onClick={deleteThis}
                className="block w-full px-4 py-2 text-left text-sm data-focus:bg-white/5 data-focus:text-white data-focus:outline-hidden"
              >
                Delete
              </button>
            </MenuItem>
             <MenuItem>
              <button
                onClick={()=>{setEdditMode(true)}}
                className="block w-full px-4 py-2 text-left text-sm data-focus:bg-white/5 data-focus:text-white data-focus:outline-hidden"
              >
                Edit comment
              </button>
            </MenuItem>
        </div>
      </MenuItems>
    </Menu>)
    :null}</div>
        </div>
        </div>
    </div>
    <div className=" my-1 ">
      {
      isInEdditMode?
     (
          <CommentEdditor commentdata={data} SetComments={SetComments}
             SetEdditMode={setEdditMode}  allComments={allComments}/>
     )
     :<p className="mx-2 whitespace-pre-line">{data.commentText}</p>
}
    </div>
    
    </div>
  )
}

export default MyComment