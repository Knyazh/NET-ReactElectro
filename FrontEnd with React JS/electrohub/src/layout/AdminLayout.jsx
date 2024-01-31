// AdminLayout.js
import React from 'react';
import AdminSideBar from '../admin/components/Sidebar/AdminSideBar';

const AdminLayout = (props) => {
  return (
    <div>
      <AdminSideBar />
      <main style={{ width: '80%', marginLeft: '300px' }}>{props.children}</main>

    </div>
  );
};

export default AdminLayout;
