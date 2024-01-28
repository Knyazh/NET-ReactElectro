import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "./categoriesandproducts.css";
import { TfiAngleRight } from "react-icons/tfi";
import { LiaCartArrowDownSolid } from "react-icons/lia";
import { IoIosGitCompare } from "react-icons/io";
import { CiHeart } from "react-icons/ci";
import axios from "axios";

const CategoriesAndProducts = () => {
  const [allProducts, setAllProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [searchQuery, setSearchQuery] = useState("");

  useEffect(() => {
    const fetchData = async () => {
      try {
        const productsResponse = await axios.get("https://fakestoreapi.com/products");
        const categoriesResponse = await axios.get("https://fakestoreapi.com/products/categories");
        setAllProducts(productsResponse.data);
        setCategories(categoriesResponse.data);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  const handleCategoryClick = (category) => {
    setSelectedCategory(category);
  };

  const handleSearchInputChange = (event) => {
    setSearchQuery(event.target.value);
  };

  const filteredProducts = allProducts.filter((product) => {
    if (selectedCategory && product.category !== selectedCategory) {
      return false; 
    }
    if (searchQuery && !product.title.toLowerCase().includes(searchQuery.toLowerCase())) {
      return false; 
    }
    return true; 
  });

  return (
    <div className="container main-product-div d-flex">
      <div className="sidebar-wrap col-lg-2">
        <div className="sidebar">
          <aside>
            <span>Browse Categories</span>
            <ul>
              {categories.map((category) => (
                <li key={category}>
                  <Link onClick={() => handleCategoryClick(category)}>
                    <TfiAngleRight />
                    {category}
                  </Link>
                </li>
              ))}
            </ul>
          </aside>
        </div>
      </div>

      <div className="product-inner col-lg-10">
        <div className="search-area mb-3 w-50 mx-auto">
          <input
            type="text"
            placeholder="Search a product"
            className="form-control"
            value={searchQuery}
            onChange={handleSearchInputChange}
          />
        </div>
        <h2>Products</h2>
        <div className="d-flex flex-wrap">
          {filteredProducts.map((product) => (
            <div key={product.id} className="col-lg-3 cart-details">
              <Link to={`/details/${product.id}`}>{product.category}</Link>
              <h2>{product.title.length > 15 ? `${product.title.slice(0, 15)}...` : product.title}</h2>
              <Link to={`/details/${product.id}`}>
                <img style={{ width: '172px', height: '172px' }} src={product.image} alt={product.title} />
              </Link>
              <div className="d-flex align-items-center justify-content-between price-and-add-to-cart">
                <span className="product-cart-price">$ {product.price}</span>
                <Link className="add-to-cart-icon" >
                  <LiaCartArrowDownSolid />
                </Link>
              </div>
              <div className="for-hover-components">
                <div className="d-flex justify-content-between wish-and-comp">
                  <div><Link><CiHeart />Wishlist</Link></div>
                  <div><Link><IoIosGitCompare />Compare</Link></div>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default CategoriesAndProducts;
