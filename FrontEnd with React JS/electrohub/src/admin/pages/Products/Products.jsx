import React, { useState, useEffect } from 'react';
import axios from 'axios';
import CategoryAPI from '../../../utils/categoryApi';
import './products.css'; 

const Products = () => {
  const [products, setProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [newProduct, setNewProduct] = useState({
    name: '',
    description: '',
    price: 0,
    image: null,
    brand: '',
    category: '',
  });

  useEffect(() => {
    axios.get('example.api/products')
      .then(response => {
        setProducts(response.data);
      })
      .catch(error => {
        console.error('Error fetching products', error);
      });

    axios.get(`${CategoryAPI.baseURL}/get-all`)
      .then(response => {
        setCategories(response.data);
      })
      .catch(error => {
        console.error('Error fetching categories', error);
      });
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewProduct(prevState => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleImageChange = (e) => {
    setNewProduct(prevState => ({
      ...prevState,
      image: e.target.files[0],
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const formData = new FormData();
    formData.append('name', newProduct.name);
    formData.append('description', newProduct.description);
    formData.append('price', newProduct.price);
    formData.append('image', newProduct.image);
    formData.append('brand', newProduct.brand);
    formData.append('category', newProduct.category);

    axios.post('example.api/products', formData)
      .then(response => {
        setProducts(prevProducts => [...prevProducts, response.data]);
        setNewProduct({
          name: '',
          description: '',
          price: 0,
          image: null,
          brand: '',
          category: '',
        });
      })
      .catch(error => {
        console.error('Error adding product', error);
      });
  };

  return (
    <div className="products-container">
      <h1>Product CRUD Example</h1>
      <form onSubmit={handleSubmit} className="product-form">
        <input
          type="text"
          placeholder="Name"
          name="name"
          value={newProduct.name}
          onChange={handleInputChange}
        />
        <input
          type="text"
          placeholder="Description"
          name="description"
          value={newProduct.description}
          onChange={handleInputChange}
        />
        <input
          type="number"
          placeholder="Price"
          name="price"
          value={newProduct.price}
          onChange={handleInputChange}
        />
        <input
          type="file"
          name="image"
          onChange={handleImageChange}
        />
        <input
          type="text"
          placeholder="Brand"
          name="brand"
          value={newProduct.brand}
          onChange={handleInputChange}
        />
        <select
          name="category"
          value={newProduct.category}
          onChange={handleInputChange}
        >
          <option value="">Select a category</option>
          {categories.map(category => (
            <option key={category.id} value={category.id}>
              {category.name}
            </option>
          ))}
        </select>
        <button type="submit">Add Product</button>
      </form>
      <div className="product-list">
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Description</th>
              <th>Price</th>
              <th>Image</th>
              <th>Brand</th>
              <th>Category</th>
            </tr>
          </thead>
          <tbody>
            {products.map(product => (
              <tr key={product.id}>
                <td>{product.name}</td>
                <td>{product.description}</td>
                <td>{product.price}</td>
                <td><img src={`path/to/local/images/${product.image}`} alt={product.name} /></td>
                <td>{product.brand}</td>
                <td>{product.category}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Products;
