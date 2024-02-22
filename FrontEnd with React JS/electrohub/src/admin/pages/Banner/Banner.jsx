import axios from 'axios';
import React, { useEffect, useState } from 'react';
import BannerApi from '../../../utils/bannerApi';

const Banner = () => {
    const [banners, setBanners] = useState([]);
    const [newBanner, setNewBanner] = useState({
      name: '',
      description: '',
      file: null
    });
    const [selectedBannerId, setSelectedBannerId] = useState(null);
  
    useEffect(() => {
      fetchBanners();
    }, []);
  
    const fetchBanners = async () => {
      try {
        const response = await axios.get(`${BannerApi.baseURL}/get-all`);
        setBanners(response.data);
      } catch (error) {
        console.error('Error fetching banners:', error);
      }
    };
  
    const addBanner = async () => {
        try {
          const formData = new FormData();
          formData.append('name', newBanner.name);
          formData.append('description', newBanner.description);
          formData.append('file', newBanner.file);
      
          await axios.post(`${BannerApi.baseURL}/add`, formData);
          fetchBanners();
          setNewBanner({ name: '', description: '', file: null });
        } catch (error) {
          console.error('Error adding banner:', error);
        }
      };
      
  
      const updateBanner = async () => {
        try {
          const formData = new FormData();
          formData.append('name', newBanner.name);
          formData.append('description', newBanner.description);
          formData.append('file', newBanner.file);
      
          await axios.put(`${BannerApi.baseURL}/update/${selectedBannerId}`, formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          });
          fetchBanners();
          setNewBanner({ name: '', description: '', file: null });
          setSelectedBannerId(null);
        } catch (error) {
          console.error('Error updating banner:', error);
        }
      };
      
    const deleteBanner = async (id) => {
      try {
        await axios.delete(`${BannerApi.baseURL}/delete/${id}`);
        fetchBanners();
      } catch (error) {
        console.error('Error deleting banner:', error);
      }
    };
  
    const handleInputChange = (event) => {
      const { name, value } = event.target;
      setNewBanner({ ...newBanner, [name]: value });
    };

    const handleImageChange = (e) => {
        setNewBanner({ ...newBanner, file: e.target.files[0] });
      };
  
    const handleUpdateClick = (banner) => {
      setSelectedBannerId(banner.id);
      setNewBanner({ name: banner.name, description: banner.description, file: banner.file });
    };
  
    return (
      <div>
        <h1>Banner CRUD</h1>
        <h2>Add Banner</h2>
        <form onSubmit={(e) => { e.preventDefault(); addBanner(); }}>
          <div className="mb-3">
            <label className="form-label">Name:</label>
            <input className='form-control' type="text" name="name" value={newBanner.name} onChange={handleInputChange} />
          </div>
          <div className="mb-3">
            <label className="form-label">Description:</label>
            <input className='form-control' type="text" name="description" value={newBanner.description} onChange={handleInputChange} />
          </div>
          <div className="mb-3">
            <label className="form-label">Image:</label>
            <input className='form-control' type="file" name="image" onChange={handleImageChange} />
          </div>
          <button className='btn btn-success' type="submit">Add Banner</button>
        </form>
        <h2>Banners</h2>
        <ul className="list-group">
          {banners.map((banner) => (
            <li key={banner.id} className="list-group-item d-flex justify-content-between align-items-center">
              <h5>{banner.name}</h5>
                <p>{banner.description}</p>
                {banner.image && <img src={banner.file} alt="Banner" style={{ maxWidth: '100px', maxHeight: '100px' }} />}
              <div>
                <button className='btn btn-danger me-2' onClick={() => deleteBanner(banner.id)}>Delete</button>
                <button className='btn btn-warning' onClick={() => handleUpdateClick(banner)}>Update</button>
              </div>
            </li>
          ))}
        </ul>
        {selectedBannerId && (
          <div>
            <h2>Update Banner</h2>
            <form onSubmit={(e) => { e.preventDefault(); updateBanner(); }}>
              <div className="mb-3">
                <label className="form-label">Name:</label>
                <input className='form-control' type="text" name="name" value={newBanner.name} onChange={handleInputChange} />
              </div>
              <div className="mb-3">
                <label className="form-label">Description:</label>
                <input className='form-control' type="text" name="description" value={newBanner.description} onChange={handleInputChange} />
              </div>
              <div className="mb-3">
                <label className="form-label">Image:</label>
                <input className='form-control' type="file" name="image" onChange={handleImageChange} />
              </div>
              <button className='btn btn-success' type="submit">Update Banner</button>
            </form>
          </div>
        )}
      </div>
    );
}

export default Banner;
