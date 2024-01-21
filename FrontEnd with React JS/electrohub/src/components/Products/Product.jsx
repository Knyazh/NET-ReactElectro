import React from "react";
import { Link } from "react-router-dom";
import "./product.css";
import iphone from "../../images/Carousel/iphone-cart.jpg";
import { LiaCartArrowDownSolid } from "react-icons/lia";
import { IoIosGitCompare  } from "react-icons/io";
import { CiHeart } from "react-icons/ci";


const Product = () => {
  return (
    <div className="container product-cart">
      <div className="d-flex justify-content-center product-nav ">
        <Link>Featured</Link>
        <Link>On Sale</Link>
        <Link>Top Rated</Link>
      </div>

      <div>
        <div className="d-flex  for-border-right">
          <div className="col-lg-2 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <img src={iphone} alt="iphone" />
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
          <div className="col-lg-2 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <img src={iphone} alt="iphone" />
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
          <div className="col-lg-2 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <img src={iphone} alt="iphone" />
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
          <div className="col-lg-2 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <img src={iphone} alt="iphone" />
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
          <div className="col-lg-2 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <img src={iphone} alt="iphone" />
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
          <div className="col-lg-2 cart-details">
            <Link href="">category</Link>
            <h2> Product Title</h2>
            <img src={iphone} alt="iphone" />
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

export default Product;
