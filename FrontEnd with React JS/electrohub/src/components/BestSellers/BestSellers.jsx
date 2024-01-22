import React from "react";
import "./bestseller.css";
import { Link } from "react-router-dom";

import iphone from "../../images/Carousel/iphone-cart.jpg";
import { LiaCartArrowDownSolid } from "react-icons/lia";
import { IoIosGitCompare } from "react-icons/io";
import { CiHeart } from "react-icons/ci";

import mainimg from "../../images/Carousel/consal (1).jpg";
import consoal1 from "../../images/Carousel/consoal1.jpg";
import consoal2 from "../../images/Carousel/consoal2.jpg";

const BestSellers = () => {
  return (
    <section className="bestseller-section">
      <div className="container">
        <header className="d-flex justify-content-between">
          <h2 className="header-nav">Best Sellers</h2>
          <ul className="d-flex">
            <li className="bestseller-ul">
              <Link>Smart Phones & Tablets</Link>
            </li>
            <li className="bestseller-ul">
              <Link>Laptops & Computers</Link>
            </li>
            <li className="bestseller-ul">
              <Link>Video Cameras</Link>
            </li>
          </ul>
        </header>
        <div className="d-flex for-background-color">
          <div className="col-lg-8 d-flex flex-wrap">
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
          </div>
          <div className="col-lg-4 best-seller-big-image-di">
            <div className="div-flex">
              <div className="product-loop-header">
                <span>
                  <Link>Game Consoles</Link>
                </span>
                <Link>
                  <h2>Game Console Controller + USB 3.0 Cable</h2>
                </Link>
              </div>
              <div>
                <Link>
                  <img src={mainimg} alt="" className="best-seller-main-img" />
                </Link>
                <div className="d-flex best-seller-second-images-div">
                  <Link>
                    <img
                      src={consoal1}
                      alt=""
                      className="best-seller-second-images"
                    />
                  </Link>
                  <Link>
                    <img
                      src={consoal2}
                      alt=""
                      className="best-seller-second-images"
                    />
                  </Link>
                  <Link>
                    <img
                      src={mainimg}
                      alt=""
                      className="best-seller-second-images"
                    />
                  </Link>
                </div>
              </div>
              <div className="product-loop-footer">
                <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
                  <span className="product-cart-price">$ 1,200</span>
                  <Link className="add-to-cart-icon" href="">
                    <LiaCartArrowDownSolid />
                  </Link>
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
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default BestSellers;
