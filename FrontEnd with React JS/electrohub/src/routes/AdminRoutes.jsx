import React from 'react'
import { Routes,Route } from 'react-router-dom'
import AdminIndex from '../admin/pages/Index/AdminIndex'
import AdminLayout from '../layout/AdminLayout'
import Category from '../admin/pages/Categories/Category'
import Products from '../admin/pages/Products/Products'
import Colors from '../admin/pages/Colors/Colors'
import Banner from '../admin/pages/Banner/Banner'

const AdminRoutes = () => {
  return (
       <AdminLayout>
      <Routes>
        <Route path='/index' element={<AdminIndex/>}/>
        <Route path='/categories' element={<Category/>}/>
        <Route path='/products' element={<Products/>}/>
        <Route path='/colors' element={<Colors/>}/>
        <Route path='/banners' element={<Banner/>}/>
      </Routes>
    </AdminLayout>
  )
}

export default AdminRoutes