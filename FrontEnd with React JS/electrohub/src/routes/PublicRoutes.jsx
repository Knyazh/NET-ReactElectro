import React from 'react'
import Layout from '../layout/Layout'
import { Routes,Route } from 'react-router-dom'
import Index from '../pages/Index/Index'
import Product from '../pages/Product/Product'
import Login from '../pages/LogIn/Login'
import Registration from '../pages/Register/Registration'

const PublicRoutes = () => {
  return (
    <Layout>
      <Routes>
        <Route path='/' element={<Index/>}/>
        <Route path='/product' element={<Product/>}/>
        <Route path='/login' element={<Login/>} />
        <Route path='/registration' element={<Registration/>} />
      </Routes>
    </Layout>
  )
}

export default PublicRoutes