import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './adminSideBar.css';
import admin from '../../../images/Carousel/adminka.png'


const AdminSideBar = () => {

  const [activeLink, setActiveLink] = useState('');

  const handleLinkClick = (link) => {
    setActiveLink(link);
  };
  return (
    <>
      <header>
        <nav id="sidebarMenu" className="collapse d-lg-block sidebar-admin collapse bg-white">
          <div className="position-sticky">
          <div className="list-group list-group-flush mx-3 mt-4">
      <Link
        to="/"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/')}
      >
        <i className="fas fa-tachometer-alt fa-fw me-3"></i><span>Main dashboard</span>
      </Link>
      <Link
        to="/admin/index"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/admin/index' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/admin/index')}
      >
        <i className="fas fa-chart-area fa-fw me-3"></i><span>Index</span>
      </Link>
      <Link
        to="/admin/categories"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/admin/categories' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/admin/categories')}
      >
        <i className="fa-solid fa-layer-group  me-3"></i><span>Categories</span>
      </Link>
      <Link
        to="/admin/products"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/admin/products' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/admin/products')}
      >
        <i className="fa-brands fa-product-hunt fa-fw me-3"></i><span>Products</span>
      </Link>
      <Link
        to="/admin/colors"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/colors' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/admin/colors')}
      >
        <i className="fas fa-chart-pie fa-fw me-3"></i><span>Colors</span>
      </Link>
      <Link
        to="/admin/banners"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/banners' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/banners')}
      >
        <i className="fas fa-chart-bar fa-fw me-3"></i><span>Banners</span>
      </Link>
      <Link
        to="/international"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/international' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/international')}
      >
        <i className="fas fa-globe fa-fw me-3"></i><span>International</span>
      </Link>
      <Link
        to="/partners"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/partners' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/partners')}
      >
        <i className="fas fa-building fa-fw me-3"></i><span>Partners</span>
      </Link>
      <Link
        to="/calendar"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/calendar' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/calendar')}
      >
        <i className="fas fa-calendar fa-fw me-3"></i><span>Calendar</span>
      </Link>
      <Link
        to="/users"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/users' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/users')}
      >
        <i className="fas fa-users fa-fw me-3"></i><span>Users</span>
      </Link>
      <Link
        to="/sales"
        className={`list-group-item list-group-item-action py-2 ripple ${activeLink === '/sales' ? 'active' : ''}`}
        onClick={() => handleLinkClick('/sales')}
      >
        <i className="fas fa-money-bill fa-fw me-3"></i><span>Sales</span>
      </Link>
    </div>
          </div>
        </nav>

        <nav id="main-navbar" className="navbar navbar-expand-lg navbar-light bg-white fixed-top">
          <div className="container-fluid">
            <button
              className="navbar-toggler"
              type="button"
              data-mdb-toggle="collapse"
              data-mdb-target="#sidebarMenu"
              aria-controls="sidebarMenu"
              aria-expanded="false"
              aria-label="Toggle navigation"
            >
              <i className="fas fa-bars"></i>
            </button>

            <Link to="/" className="navbar-brand">
              <img
                src={admin}
                height="50"
                alt="MDB Logo"
                loading="lazy"
              />
            </Link>
            <form className="d-none d-md-flex input-group w-auto my-auto">
              <input
                autoComplete="off"
                type="search"
                className="form-control rounded"
                placeholder='Search (ctrl + "/" to focus)'
                style={{ minWidth: '225px' }}
              />
              <span className="input-group-text border-0"><i className="fas fa-search"></i></span>
            </form>
          </div>
        </nav>
      </header>

      <main style={{ marginTop: '58px' }}>
        <div className="container pt-4"></div>
      </main>
    </>
  );
};

export default AdminSideBar;
