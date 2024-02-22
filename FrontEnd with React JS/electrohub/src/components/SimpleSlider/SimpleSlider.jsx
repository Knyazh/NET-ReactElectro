import Carousel from 'react-bootstrap/Carousel';
import './simpleslider.css'
import { useEffect, useState } from 'react';
import axios from 'axios';

function SimpleSlider() {

  const [banners, setBanners] = useState([]);

  useEffect(() => {
    const getBanners = async () => {
      await axios
        .get("https://localhost:7010/api/banner/get-all")
        .then((response) => setBanners(response.data))
        .catch((err) => console.log(err));
    };

    getBanners();
  }, []);

  return (
    <Carousel fade className='carousel'>
      {banners.map((banner) => (
        <Carousel.Item key={banner.id} className='carousel-item'>
          <img
            className="d-block w-100"
            src={banner.file}
            alt="Banner"
          />
          <Carousel.Caption className='carousel-caption'>
            <h3>{banner.name}</h3>
            <p>{banner.description}</p>
          </Carousel.Caption>
        </Carousel.Item>
      ))}
    </Carousel>
  );
}

export default SimpleSlider;
