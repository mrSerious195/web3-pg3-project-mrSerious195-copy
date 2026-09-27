
import type { NewsResponseContract } from '../api/ApiResponses/NewsResponseContract'
import { useFormik } from 'formik'
import { NewsApi } from '../api/NewsApi'
import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer'
import type { NewsRequestContract } from '../api/ApiRequest/NewsRequestContract'
import * as Yup from 'yup';
import SubmitButton from './SubmitButton'
import CancelButton from './CancelButton'
import { useDarkMode } from '../hooks/useDarkMode'

interface props {
    startingdata: NewsResponseContract
    exitEdditmode: () => void
    setIndex: (index:number) => void
    setContracts: (newnewsList:NewsResponseContract[]) => void
    allContracts:NewsResponseContract[]
}

const NewsEdditor = ({ startingdata, exitEdditmode,allContracts, setContracts,setIndex}: props) => {
    const bearerToken = useHeaderWithBearer()

    const schema = Yup.object().shape({
        title: Yup.string().required(),
        body: Yup.string().required(),

    })

    const { handleChange, handleBlur, handleSubmit, values } = useFormik({
        validationSchema: schema,
        initialValues: {
            
            title: startingdata.title,
            body: startingdata.body
        },
        onSubmit: values => {
            const data: NewsRequestContract = {
                ...startingdata,
                title: values.title,
                body: values.body
            }
            NewsApi.put<NewsResponseContract>("api/News", data, bearerToken).then(
               (values)=>{ const filteredlist = allContracts.filter(x=>x.id!=startingdata.id)
                if(!filteredlist)
                    return
                  const newlist:NewsResponseContract[] = [...filteredlist,values.data]
                setContracts(newlist)
                setIndex(newlist.length-1)
                exitEdditmode()
               }
            )
        }
    })

const date = startingdata && new Date(startingdata.time);
  const dateReadable = date && date.toLocaleString("en-GB");
  const {field,highlight} = useDarkMode();
    return (
        <div>
     <form  onSubmit={handleSubmit} className={`flex flex-col items-center min-w-[500px] shadow-xl/20 ${field} m-5`}>
                <div className={`flex w-full ${highlight} font-semibold px-2 py-1 items-center justify-between`}>
                   <input id="title"
              className="border flex-1 "
              name="title"
              value={values.title}
              onChange={handleChange}
              onBlur={handleBlur}
            />
                    <div>{dateReadable && dateReadable}</div>
                </div>
                <div className="w-full flex px-2 py-1"> <textarea
              className="flex-1 min-w-0 border "
              id="body"
              name="body"
              value={values.body}
              onChange={handleChange}
              onBlur={handleBlur}
            /></div>
            <SubmitButton/>
             <CancelButton setCurrentMode={()=>{exitEdditmode()}} />
            </form>
 </div> 
    )
}

export default NewsEdditor