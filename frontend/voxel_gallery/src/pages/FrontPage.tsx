
import SearchGrid from '../components/SearchGrid.tsx';
import NewsGrid from '../components/NewsGrid.tsx';
import FavoriteGrid from '../components/FavoriteGrid.tsx';





const FrontPage = () => {

  return (
    <>
    <div className="flex max-w-screen">
      <NewsGrid className="flex-1 max-w-1/2"/>
      <FavoriteGrid className="flex-1"/>
    </div>
    <SearchGrid/>
    
 
  </>
  )
}

export default FrontPage