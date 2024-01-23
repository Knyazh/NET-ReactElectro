import React from 'react'
import Layout from '../layout/Layout'
import { Routes,Route } from 'react-router-dom'
import Index from '../pages/Index/Index'
import Product from '../pages/Product/Product'
import Login from '../pages/LogIn/Login'

const PublicRoutes = () => {
  return (
    <Layout>
      <Routes>
        <Route path='/' element={<Index/>}/>
        <Route path='/product' element={<Product/>}/>
        <Route path='/login' element={<Login/>} />
      </Routes>
    </Layout>
  )
}

export default PublicRoutes