import { BrowserRouter } from 'react-router-dom';
import './App.css';
import PublicRoutes from './routes/PublicRoutes';
import { ProductProvider } from './context/ProductContext';

function App() {
  return (
    <div  className='App'>
      <BrowserRouter>
      <ProductProvider>
         <PublicRoutes/>
      </ProductProvider>
      </BrowserRouter>
    </div>
  );
}

export default App;
