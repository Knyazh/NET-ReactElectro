import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

import './App.css';
import { ShopContextProvider } from './context/shop-context';
import PublicRoutes from './routes/PublicRoutes';
import AdminRoutes from './routes/AdminRoutes';

function App() {
  return (
    <div className='App'>
      <BrowserRouter>
        <ShopContextProvider>
          <Routes>
            <Route path="/admin/*" element={<AdminRoutes />} />
            <Route path="/*" element={<PublicRoutes />} />
          </Routes>
        </ShopContextProvider>
      </BrowserRouter>
    </div>
  );
}

export default App;
