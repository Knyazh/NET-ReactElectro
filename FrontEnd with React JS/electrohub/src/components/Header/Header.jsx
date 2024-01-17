import React from 'react'
import "./header.css"
import { Link, NavLink } from 'react-router-dom'

const Header = () => {
  return (
    <div>
        <header className='container'>
            <div className='row'>
                <div className='header-top d-flex align-items-center justify-content-between '>
                    <div className='main-logo col-lg-2'>
                        <p>electro </p>
                    </div>
                    <div className='navbar-search col-lg-8 d-flex justify-content-between'>
                        <div className='input-search'>
                            <input type="text" className='form-control border-0' placeholder='Search for Products' />
                        </div>
                        <div></div>
                        <div className="select-section ">
                        <select class="form-select border-0" aria-label="Default select example">
                        <option selected>All Categories</option>
                        <option value="1">Phones</option>
                        <option value="2">Laptops</option>
                        <option value="3">HeadPhones</option>
                        <option value="4">Cameras</option>
                        </select>
                        </div>
                        <div className="select-categories ">
                            <button type='submit' className='btn btn-warning btn-sm border-0'>
                            <i class="fa-solid fa-magnifying-glass"></i>
                            </button>
                        </div>
                    </div>
                    <div className='navbar-icons col-lg-2'>
                        <nav>
                            <NavLink to='/'><i class="fa-solid fa-code-compare"></i></NavLink>
                            <NavLink to='/'><i class="fa-regular fa-heart"></i></NavLink>
                            <NavLink to='/product'><i class="fa-regular fa-user"></i></NavLink>
                            <NavLink to='/product'><i class="fa-solid fa-cart-shopping"></i></NavLink>
                        </nav>
                    </div>
                </div>
                <div className='header-mid'>

                </div>
            </div>
        </header>
    </div>
  )
}

export default Header