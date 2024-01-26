import React from 'react'
import Layout from '../layout/Layout'
import { Routes,Route } from 'react-router-dom'
import Index from '../pages/Index/Index'
import Product from '../pages/Product/Product'
import Login from '../pages/LogIn/Login'
import Registration from '../pages/Register/Registration'
import Details  from '../pages/Details/Details'
import CartDetails from '../pages/CartDetails/CartDetails'

const PublicRoutes = () => {
  return (
    <Layout>
      <Routes>
        <Route path='/' element={<Index/>}/>
        <Route path='/products' element={<Product/>}/>
        <Route path='/login' element={<Login/>} />
        <Route path='/registration' element={<Registration/>} />
        <Route path='/details' element={<Details/>} />
        <Route path='/cartdetails' element={<CartDetails/>} />

      </Routes>
    </Layout>
  )
}

export default PublicRoutes