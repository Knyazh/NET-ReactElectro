import React, { useState, useEffect } from 'react';
import axios from 'axios';
import CategoryAPI from '../../../utils/categoryApi';

const Category = () => {
  const [categories, setCategories] = useState([]);
  const [newCategory, setNewCategory] = useState({
    name: '',
    description: ''
  });
  const [selectedCategoryId, setSelectedCategoryId] = useState(null);

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const response = await axios.get(`${CategoryAPI.baseURL}/get-all`);
      setCategories(response.data);
    } catch (error) {
      console.error('Error fetching products:', error);
    }
  };

  const addCategory = async () => {
    try {
      await axios.post(`${CategoryAPI.baseURL}/add-category`, newCategory);
      fetchCategories();
      setNewCategory({ name: '', description: '' });
    } catch (error) {
      console.error('Error adding product:', error);
    }
  };

  const updateCategory = async () => {
    try {
      await axios.put(`${CategoryAPI.baseURL}/update-category/${selectedCategoryId}`, newCategory);
      fetchCategories();
      setNewCategory({ name: '', description: '' });
      setSelectedCategoryId(null);
    } catch (error) {
      console.error('Error updating product:', error);
    }
  };
  const deleteCategory = async (id) => {
    try {
      await axios.delete(`${CategoryAPI.baseURL}/delete-category-id/${id}`);
      fetchCategories();
    } catch (error) {
      console.error('Error deleting product:', error);
    }
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setNewCategory({ ...newCategory, [name]: value });
  };

  const handleUpdateClick = (category) => {
    setSelectedCategoryId(category.id);
    setNewCategory({ name: category.name, description: category.description });
  };

  return (
    <div>
      <h1>Categories CRUD</h1>
      <h2>Add Categories</h2>
      <form onSubmit={(e) => { e.preventDefault(); addCategory(); }}>
        <div className="mb-3">
          <label className="form-label">Name:</label>
          <input className='form-control' type="text" name="name" value={newCategory.name} onChange={handleInputChange} />
        </div>
        <div className="mb-3">
          <label className="form-label">Description:</label>
          <input className='form-control' type="text" name="description" value={newCategory.description} onChange={handleInputChange} />
        </div>
        <button className='btn btn-success' type="submit">Add Category</button>
      </form>
      <h2>Categories</h2>
      <ul className="list-group">
        {categories.map((category) => (
          <li key={category.id} className="list-group-item d-flex justify-content-between align-items-center">
            {category.name} - {category.description}
            <div>
              <button className='btn btn-danger me-2' onClick={() => deleteCategory(category.id)}>Delete</button>
              <button className='btn btn-warning' onClick={() => handleUpdateClick(category)}>Update</button>
            </div>
          </li>
        ))}
      </ul>
      {selectedCategoryId && (
        <div>
          <h2>Update Category</h2>
          <form onSubmit={(e) => { e.preventDefault(); updateCategory(); }}>
            <div className="mb-3">
              <label className="form-label">Name:</label>
              <input className='form-control' type="text" name="name" value={newCategory.name} onChange={handleInputChange} />
            </div>
            <div className="mb-3">
              <label className="form-label">Description:</label>
              <input className='form-control' type="text" name="description" value={newCategory.description} onChange={handleInputChange} />
            </div>
            <button className='btn btn-success' type="submit">Update Category</button>
          </form>
        </div>
      )}
    </div>
  );
};

export default Category;
