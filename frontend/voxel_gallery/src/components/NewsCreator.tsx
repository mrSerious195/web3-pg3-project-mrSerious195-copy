
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
    exitEdditmode: () => void
    setIndex: (index:number) => void
    setContracts: (newnewsList:NewsResponseContract[]) => void
    allContracts:NewsResponseContract[]
}

const NewsCreator = ({ exitEdditmode,allContracts, setContracts,setIndex}: props) => {
    const bearerToken = useHeaderWithBearer()

    const schema = Yup.object().shape({
        title: Yup.string().required(),
        body: Yup.string().required(),

    })

    const { handleChange, handleBlur, handleSubmit, values } = useFormik({
        validationSchema: schema,
        initialValues: {
            
            title: "",
            body: ""
        },
        onSubmit: values => {
            const data: NewsRequestContract = {
                title: values.title,
                body: values.body
            }
            NewsApi.post<NewsResponseContract>("api/News", data, bearerToken).then(
               (values)=>{ 
                  const newlist:NewsResponseContract[] = [...allContracts,values.data]
                setContracts(newlist)
                setIndex(newlist.length-1)
                exitEdditmode()
               }
            )
        }
    })
const {highlight,field} = useDarkMode()
    return (
        <div>
     <form  onSubmit={handleSubmit} className={`flex flex-col items-center min-w-[500px] shadow-xl/20 ${field} m-5`}>
                <div className={`flex w-full ${highlight}  text-white font-semibold px-2 py-1 items-center justify-between`}>
                   <input id="title"
              className="border flex-1 "
              name="title"
              value={values.title}
              onChange={handleChange}
              onBlur={handleBlur}
            />
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

export default NewsCreator