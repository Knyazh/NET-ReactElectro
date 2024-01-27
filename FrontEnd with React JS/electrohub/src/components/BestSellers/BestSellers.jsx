import React, { useContext } from "react";
import "./bestseller.css";
import { Link } from "react-router-dom";

import { LiaCartArrowDownSolid } from "react-icons/lia";
import { IoIosGitCompare } from "react-icons/io";
import { CiHeart } from "react-icons/ci";
import { ShopContext } from "../../context/shop-context";



const BestSellers = (props) => {

  const { id, title, price, image,category } = props.data;
  const { addToCart, cartItems } = useContext(ShopContext);

  const cartItemCount = cartItems[id];


  return (
    <>
       <div  className="col-lg-3 cart-details">
    <Link href="">{category}</Link>
    <h2>
    {title.length > 15 ? `${title.substring(0, 15)}...` : title}
    </h2>
    <div className="product-image-container d-flex">
      <Link>
        <img
          className="product-image"
          src={image}
          alt="iphone"
        />
      </Link>
    </div>
    <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
      <span className="product-cart-price">
        $ {price}
      </span>
      <button className="add-to-cart-icon" href="" onClick={()=> addToCart(id)}>
        
        <LiaCartArrowDownSolid />
         

        {cartItemCount > 0 && <> ({cartItemCount})</>}
      

      </button>
    </div>
    <div className="for-hover-components">
      <div className="d-flex justify-content-between wish-and-comp">
        <div>
          <Link>
            <CiHeart />
            Wishlist
          </Link>
        </div>
        <div>
          <Link>
            <IoIosGitCompare />
            Compare
          </Link>
        </div>
      </div>
    </div>
  </div>
    </>
  );
};

export default BestSellers;
