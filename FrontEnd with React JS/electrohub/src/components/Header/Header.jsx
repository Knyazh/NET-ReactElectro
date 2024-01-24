import React from "react";
import "./header.css";
import { Link, NavLink } from "react-router-dom";

const Header = () => {
  return (
    <div>
      <header className="container">
        <div className="row">
        <div className="header-top d-flex align-items-center justify-content-between ">
            <div className="main-logo col-lg-2">
              <p>electro </p>
            </div>
            <div className="navbar-search col-lg-8 d-flex justify-content-between">
              <div className="input-search">
                <input
                  type="text"
                  className="form-control border-0"
                  placeholder="Search for Products"
                />
              </div>
              <div className="select-section">
                <select
                  defaultValue="All Categories"
                  className="form-select border-0"
                  aria-label="Default select example"
                >
                  <option value="All Categories">All Categories</option>
                  <option value="Phones">Phones</option>
                  <option value="Laptops">Laptops</option>
                  <option value="HeadPhones">HeadPhones</option>
                  <option value="Cameras">Cameras</option>
                </select>
              </div>
              <div className="select-categories">
                <button
                  type="submit"
                  className="btn btn-warning btn-sm border-0"
                >
                  <i className="fa-solid fa-magnifying-glass"></i>
                </button>
              </div>
            </div>
            <div className="navbar-icons col-lg-2">
              <nav className="d-flex">
                <div className="nav-link" data-title="Compare">
                  <NavLink to="/">
                    <i className="fa-solid fa-code-compare"></i>
                  </NavLink>
                </div>
                <div className="nav-link" data-title="Wishlist">
                  <NavLink to="/">
                    <i className="fa-regular fa-heart"></i>
                  </NavLink>
                </div>
                <div className="nav-link" data-title="Account">
                  <NavLink to="/login">
                    <i className="fa-regular fa-user"></i>
                  </NavLink>
                </div>

                <div className="nav-link" data-title="Register">
                  <NavLink to="/registration">
                  <i class="fa-regular fa-address-card"></i>
                  </NavLink>
                </div>
                <div className="nav-link" data-title="Cart">
                  <NavLink to="/product">
                    <i className="fa-solid fa-cart-shopping"></i>
                  </NavLink>
                </div>
              </nav>
            </div>
          </div>
        </div>
      </header>
          <div className="header-mid">

            <div className="container">
              <div className="row ">
                <ul className="d-flex justify-content-between">
                  <Link to='/'>Home</Link>
                  <Link to='/products'>Tv & Audio</Link>
                  <Link>Smart Phones</Link>
                  <Link>Laptop & Desktops</Link>
                  <Link>Gadgets</Link>
                  <Link>GPS&Car</Link>
                  <Link>Cameras & Accessories</Link>
                  <Link>Movie & Games</Link>
                </ul>
              </div>
            </div>
          </div>
    </div>
  );
};

export default Header;
