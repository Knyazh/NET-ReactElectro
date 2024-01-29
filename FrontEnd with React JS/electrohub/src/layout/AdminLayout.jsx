import React from 'react'
import AdminSideBar from '../admin/components/Sidebar/AdminSideBar'

const AdminLayout = (props) => {
  return (
       <div>
        <AdminSideBar/>
        <main>
            {props.children}
        </main>
    </div>
  )
}

export default AdminLayout