import React from 'react'
import Layout from '../layout/Layout'
import { Routes,Route } from 'react-router-dom'
import Index from '../pages/Index/Index'
import Product from '../pages/Product/Product'

const PublicRoutes = () => {
  return (
    <Layout>
      <Routes>
        <Route path='/' element={<Index/>}/>
        <Route path='/product' element={<Product/>}/>
      </Routes>
    </Layout>
  )
}

export default PublicRoutes