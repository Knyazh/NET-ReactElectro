import React, { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "./product.css";
import { LiaCartArrowDownSolid } from "react-icons/lia";
import { IoIosGitCompare } from "react-icons/io";
import { CiHeart } from "react-icons/ci";
import axios from "axios";
import { ProductContext } from "../../context/ProductContext";

const Product = () => {
  const [products, setProducts] = useState([]);
  const context = useContext(ProductContext)

const addToCardHandler = (data) =>{
  context.addToCart(data)

}

  useEffect(() => {
    const getProducts = async () => {
      await axios
        .get("https://fakestoreapi.com/products?limit=6")
        .then((response) => setProducts(response.data))
        .catch((err) => console.log(err));
    };

    getProducts();
  }, []);

  return (
    <div  key={products.id}  className="container product-cart">
      <div className="d-flex justify-content-center product-nav ">
        <Link>Featured</Link>
        <Link>On Sale</Link>
        <Link>Top Rated</Link>
      </div>

      <div>
        <div className="d-flex  for-border-right">
          {products &&
            products.map((product) => {
              return (
                <div data={product} key={product.id} className="col-lg-2 cart-details">
                  <Link href="">{product.category}</Link>
                  <h2>
                    {product.title.length > 15
                      ? `${product.title.slice(0, 15)}...`
                      : product.title}
                  </h2>
                  <div className="product-image-container d-flex">
                    <Link>
                      <img
                        className="product-image"
                        src={product.image}
                        alt="iphone"
                      />
                    </Link>
                  </div>
                  <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
                    <span className="product-cart-price">
                      $ {product.price}
                    </span>
                    <button onClick={() => addToCardHandler({
                        Id: product.id,
                        Image : product.image,
                        Title : product.title,
                        Category : product.category,
                        Price: product.price

                    })} className="add-to-cart-icon" href="">
                      <LiaCartArrowDownSolid />
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
              );
            })}
        </div>
      </div>
    </div>
  );
};

export default Product;
