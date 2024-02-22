import React, { useState, useEffect } from 'react';
import axios from 'axios';
import CategoryAPI from '../../../utils/categoryApi';
import './products.css'; 
import ProductApi from '../../../utils/productApi';

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
  const [updateProductId, setUpdateProductId] = useState(null);

  useEffect(() => {
    axios.get('https://localhost:7010/api/v1/product')
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

    axios.post(`${ProductApi.baseURL}`, formData)
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

  const handleDelete = (id) => {
    axios.delete(`${ProductApi.baseURL}/${id}`)
      .then(() => {
        setProducts(prevProducts => prevProducts.filter(product => product.id !== id));
      })
      .catch(error => {
        console.error('Error deleting product', error);
      });
  };

  const handleUpdate = (id) => {
    const updatedProduct = products.find(product => product.id === id);
    setNewProduct({
      name: updatedProduct.name,
      description: updatedProduct.description,
      price: updatedProduct.price,
      image: updatedProduct.image,
      brand: updatedProduct.brand,
      category: updatedProduct.category,
    });
    setUpdateProductId(id);
  };

  const updateProduct = async () => {
    const formData = new FormData();
    formData.append('name', newProduct.name);
    formData.append('description', newProduct.description);
    formData.append('price', newProduct.price);
    formData.append('image', newProduct.image);
    formData.append('brand', newProduct.brand);
    formData.append('category', newProduct.category);

    try {
      const response = await axios.put(`${ProductApi.baseURL}/${updateProductId}`, formData);
      const updatedProductIndex = products.findIndex(product => product.id === updateProductId);
      const updatedProducts = [...products];
      updatedProducts[updatedProductIndex] = response.data;
      setProducts(updatedProducts);
      setUpdateProductId(null);
      setNewProduct({
        name: '',
        description: '',
        price: 0,
        image: null,
        brand: '',
        category: '',
      });
    } catch (error) {
      console.error('Error updating product', error);
    }
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
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {products.map(product => (
              <tr key={product.id}>
                <td>{product.name}</td>
                <td>{product.description}</td>
                <td>{product.price}</td>
                <td><img src={product.image} alt={product.name} /></td>
                <td>{product.brand}</td>
                <td>{product.category}</td>
                <td>
                  <button onClick={() => handleUpdate(product.id)}>Update</button>
                  <button onClick={() => handleDelete(product.id)}>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Products;
