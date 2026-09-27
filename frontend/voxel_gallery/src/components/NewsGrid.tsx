import  { useEffect, useState } from 'react'
import { NewsApi } from '../api/NewsApi';
import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer';
import { useAuth } from 'react-oidc-context';
import type { NewsResponseContract } from '../api/ApiResponses/NewsResponseContract';
import { ArrowLeftIcon, ArrowRightIcon } from '@heroicons/react/20/solid';
import { Menu, MenuButton, MenuItem, MenuItems } from '@headlessui/react';
import NewsEdditor from './NewsEdditor';
import NewsCreator from './NewsCreator';
import useAdminAuthorization from '../hooks/useAdminAuthorization';
import { useDarkMode } from '../hooks/useDarkMode';
interface props{
    className?:string
}


const NewsGrid = ({className}:props) => {
  const IsAdmin = useAdminAuthorization()
    const [index, setIndex] = useState(0);
    const [newsData, setNewsData] = useState<NewsResponseContract[]>([])
    const { isAuthenticated } = useAuth()
     const [FailedToLoad,setFailedToLoad] = useState<boolean>(false);
     //used to be an enum, but that wouldn't build
     // 0 is view
     // 1 is edit
     // 2 is new
    const [thisNewsState,setNewsState] = useState<number>(0)

    const bearer = useHeaderWithBearer()

    const search = ()=>{
      setFailedToLoad(false)
        NewsApi.get<NewsResponseContract[]>("api/News", bearer).then((data) => {
            setNewsData(data.data)
           console.log("news")
           console.log(data)}).catch(
          ()=>{setFailedToLoad(true)})
    }
    useEffect(() => {
        search()
        }
        ,[isAuthenticated])

    const deleteCurrent=()=>{
        const idToRemove = newsData[index].id
          NewsApi.delete(`api/News/${idToRemove}`, bearer).then(() => {
            const newnewsdata = newsData.filter(x=>x.id!=idToRemove)
            setNewsData(newnewsdata)
           setIndex((newsData.length + index - 1) % newsData.length)
        }
        
        )
    }



    const currentData = newsData.length == 0 ? null : newsData[index]

const date = currentData && new Date(currentData.time);


  const dateReadable = date && date.toLocaleString("en-GB");
const {field, highlight,hoverhighlight} =useDarkMode()
    return (
        <div className={className}>

            <div className="flex  items-center">



                <h2 className={` py-1 px-2  font-semibold ${highlight} ml-2`}>News</h2>
                <div className={`h-[10px] flex-3 ${highlight}`} />
                {(thisNewsState ==  0)&&  
 <div className='flex flex-4 items-center'>
 {IsAdmin?(
 <Menu as="div" className={`relative inline-block ${highlight}`}>
      <MenuButton className={` flex-1  px-3 py-1 font-semibold  ${hoverhighlight}`}>
        X
       
      </MenuButton>

      <MenuItems
        transition
        className={`absolute right-0 z-10 mt-2 w-56 origin-top-right   border-black border  
        ${highlight} transition data-closed:scale-95 data-closed:transform data-closed:opacity-0 
        data-enter:duration-100 data-enter:ease-out data-leave:duration-75 data-leave:ease-in`}
      >
        <div className="py-1">
         
            <MenuItem>
              <button
                onClick={deleteCurrent}
                className="block w-full px-4 py-2 text-left text-sm data-focus:bg-white/5 data-focus:text-white data-focus:outline-hidden"
              >
                Delete Current
              </button>
               </MenuItem>
             <MenuItem>
              <button
                onClick={()=>{setNewsState(1)}}
                className="block w-full px-4 py-2 text-left text-sm data-focus:bg-white/5 data-focus:text-white data-focus:outline-hidden"
              >
                Edit Current
              </button>
            </MenuItem>
             <MenuItem>
              <button
                onClick={()=>{setNewsState(2)}}
                className="block w-full px-4 py-2 text-left text-sm data-focus:bg-white/5 data-focus:text-white data-focus:outline-hidden"
              >
                New news item
              </button>
            </MenuItem>
        </div>
      </MenuItems>
    </Menu>)
  :null}
 <div className={`h-[10px] flex-3 ${highlight}`}/>
           

                <div className={`flex items-center  justify-center ${highlight}`}>
                    <button onClick={() => setIndex((newsData.length + index - 1) % newsData.length)}>
                        <ArrowLeftIcon className={`w-8 ${hoverhighlight}`} />
                    </button>
                    <p className=" text-white text-center">{index+1}</p>
                    <button onClick={() => setIndex((index + 1) % newsData.length)}>
                        <ArrowRightIcon className={`w-8 ${hoverhighlight}`}/>
                    </button >
                </div>
            </div>}
            </div>
            
            {(thisNewsState ==  0)&&  <div className={`flex flex-col items-center min-w-[500px] shadow-xl/20 m-5 ${field}`}>
             {(FailedToLoad&&(newsData.length<=0))&&<button className={`p-3 ${highlight} ${hoverhighlight}`} onClick={search}>Retry Loading</button>}
                {(!FailedToLoad&&(newsData.length<=0))&&<span className="loading loading-spinner text-primary"></span>}
                <div className={`flex w-full font-semibold px-2 py-1 items-center justify-between ${highlight}`}>
                    <div>{currentData && currentData.title}</div>
                    <div>{dateReadable && dateReadable}</div>
                </div>
                <div className="w-full min-w-0 px-2 py-1"><p className="whitespace-pre-line  break-words">{currentData && currentData.body}</p></div>
            </div>}
            {(thisNewsState ==  2)&& <NewsCreator exitEdditmode={() => { setNewsState(0); } }
            setIndex={setIndex} setContracts={setNewsData} allContracts={newsData}/>}
            {(thisNewsState ==  1)&&  
            <NewsEdditor startingdata={newsData[index]}
            exitEdditmode={() => { setNewsState(0); } }
            setIndex={setIndex} setContracts={setNewsData} allContracts={newsData}/>}
        </div>
    )
}

export default NewsGrid