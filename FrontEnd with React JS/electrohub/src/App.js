import { BrowserRouter } from 'react-router-dom';
import './App.css';
import PublicRoutes from './routes/PublicRoutes';
import { ShopContextProvider } from './context/shop-context';

function App() {
  return (
    <div  className='App'>
      <BrowserRouter>
      <ShopContextProvider>

         <PublicRoutes/>
      </ShopContextProvider>
      </BrowserRouter>
    </div>
  );
}

export default App;
