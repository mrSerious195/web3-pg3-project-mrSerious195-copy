import { useFormik } from 'formik'
import { NewsApi } from '../api/NewsApi'
import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer'
import type {NewsRequestContract} from '../api/ApiRequest/NewsRequestContract'

const NewsInput = () => {
    const bearerToken = useHeaderWithBearer();
  const { handleChange, handleBlur, handleSubmit, values } = useFormik({
        initialValues: {
         Title:"",
         Body:""
        },
        onSubmit: values => {
            const data:NewsRequestContract = {
                title: values.Title,
                body: values.Body
            }
            NewsApi.post("api/News",data,bearerToken)
        }
    })
  
  
  
  
  
    return (
        <div>
    <h2>Post new Site News</h2>
      <form onSubmit={handleSubmit} className=" flex-1-75 border rounded-lg p-2">
         <label htmlFor='Title'>Title</label>
          <input id="Title:"
            className=" w-full border  resize"
            name="Title"
            value={values.Title}
            onChange={handleChange}
            onBlur={handleBlur}
          />
           <label htmlFor='Body'>Body</label>
          <textarea id="Body:"
            className=" w-full border  resize"
            name="Body"
            value={values.Body}
            onChange={handleChange}
            onBlur={handleBlur}
          />
           <button type="submit">Submit</button>
      </form>
    </div>

  )
}

export default NewsInput