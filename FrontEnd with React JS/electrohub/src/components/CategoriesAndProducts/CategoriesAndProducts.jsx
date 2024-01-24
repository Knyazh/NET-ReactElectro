import React from "react";
import { Link } from "react-router-dom";
import "./categoriesandproducts.css";
import { TfiAngleRight } from "react-icons/tfi";
import { LiaCartArrowDownSolid } from "react-icons/lia";
import { IoIosGitCompare  } from "react-icons/io";
import { CiHeart } from "react-icons/ci";
import iphone from '../../images/Carousel/iphone-cart.jpg';


const CategoriesAndProducts = () => {
  return (
    <div className="container main-product-div d-flex ">
      <div className="sidebar-wrap col-lg-2">
        <div className="sidebar">
          <aside>
            <span>Browse Categories</span>
            <ul>
              <li>
                <Link><TfiAngleRight />Accessories</Link>
              </li>
              <li>
                <Link><TfiAngleRight />Cameras</Link>
              </li>
              <li>
                <Link><TfiAngleRight />Computers</Link>
              </li>
              <li>
                <Link><TfiAngleRight />Phones</Link>
              </li>
              <li>
                <Link><TfiAngleRight />Headphones</Link>
              </li>
              <li>
                <Link><TfiAngleRight />Laptops</Link>
              </li>
              <li>
                <Link><TfiAngleRight />Mouses</Link>
              </li>
              <li>
                <Link><TfiAngleRight />Tv & Audio</Link>
              </li>
              
            </ul>
          </aside>
        </div>


      </div>

      <div className="product-inner col-lg-10">
        <h2>Products</h2>
        <div className="d-flex flex-wrap"> 

        <div className="col-lg-3 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <Link>
            <img src={iphone} alt="iphone" />
            </Link>
            <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
              <span className="product-cart-price">$ 1,200</span>
              <Link className="add-to-cart-icon" href="">
                <LiaCartArrowDownSolid />
              </Link>
            </div>
            <div className="for-hover-components">
          <div className="d-flex justify-content-between wish-and-comp">
            <div><Link><CiHeart/>Wishlist</Link></div>
            <div><Link><IoIosGitCompare/>Compare</Link></div>
          </div>
            </div>
          </div>
          <div className="col-lg-3 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <Link>
            <img src={iphone} alt="iphone" />
            </Link>
            <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
              <span className="product-cart-price">$ 1,200</span>
              <Link className="add-to-cart-icon" href="">
                <LiaCartArrowDownSolid />
              </Link>
            </div>
            <div className="for-hover-components">
          <div className="d-flex justify-content-between wish-and-comp">
            <div><Link><CiHeart/>Wishlist</Link></div>
            <div><Link><IoIosGitCompare/>Compare</Link></div>
          </div>
            </div>
          </div>
          <div className="col-lg-3 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <Link>
            <img src={iphone} alt="iphone" />
            </Link>
            <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
              <span className="product-cart-price">$ 1,200</span>
              <Link className="add-to-cart-icon" href="">
                <LiaCartArrowDownSolid />
              </Link>
            </div>
            <div className="for-hover-components">
          <div className="d-flex justify-content-between wish-and-comp">
            <div><Link><CiHeart/>Wishlist</Link></div>
            <div><Link><IoIosGitCompare/>Compare</Link></div>
          </div>
            </div>
          </div>
          <div className="col-lg-3 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <Link>
            <img src={iphone} alt="iphone" />
            </Link>
            <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
              <span className="product-cart-price">$ 1,200</span>
              <Link className="add-to-cart-icon" href="">
                <LiaCartArrowDownSolid />
              </Link>
            </div>
            <div className="for-hover-components">
          <div className="d-flex justify-content-between wish-and-comp">
            <div><Link><CiHeart/>Wishlist</Link></div>
            <div><Link><IoIosGitCompare/>Compare</Link></div>
          </div>
            </div>
          </div>
          <div className="col-lg-3 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <Link>
            <img src={iphone} alt="iphone" />
            </Link>
            <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
              <span className="product-cart-price">$ 1,200</span>
              <Link className="add-to-cart-icon" href="">
                <LiaCartArrowDownSolid />
              </Link>
            </div>
            <div className="for-hover-components">
          <div className="d-flex justify-content-between wish-and-comp">
            <div><Link><CiHeart/>Wishlist</Link></div>
            <div><Link><IoIosGitCompare/>Compare</Link></div>
          </div>
            </div>
          </div>
          <div className="col-lg-3 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <Link>
            <img src={iphone} alt="iphone" />
            </Link>
            <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
              <span className="product-cart-price">$ 1,200</span>
              <Link className="add-to-cart-icon" href="">
                <LiaCartArrowDownSolid />
              </Link>
            </div>
            <div className="for-hover-components">
          <div className="d-flex justify-content-between wish-and-comp">
            <div><Link><CiHeart/>Wishlist</Link></div>
            <div><Link><IoIosGitCompare/>Compare</Link></div>
          </div>
            </div>
          </div>
          <div className="col-lg-3 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <Link>
            <img src={iphone} alt="iphone" />
            </Link>
            <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
              <span className="product-cart-price">$ 1,200</span>
              <Link className="add-to-cart-icon" href="">
                <LiaCartArrowDownSolid />
              </Link>
            </div>
            <div className="for-hover-components">
          <div className="d-flex justify-content-between wish-and-comp">
            <div><Link><CiHeart/>Wishlist</Link></div>
            <div><Link><IoIosGitCompare/>Compare</Link></div>
          </div>
            </div>
          </div>
          <div className="col-lg-3 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <Link>
            <img src={iphone} alt="iphone" />
            </Link>
            <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
              <span className="product-cart-price">$ 1,200</span>
              <Link className="add-to-cart-icon" href="">
                <LiaCartArrowDownSolid />
              </Link>
            </div>
            <div className="for-hover-components">
          <div className="d-flex justify-content-between wish-and-comp">
            <div><Link><CiHeart/>Wishlist</Link></div>
            <div><Link><IoIosGitCompare/>Compare</Link></div>
          </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CategoriesAndProducts;
