import Carousel from 'react-bootstrap/Carousel';
import './simpleslider.css'
import img1 from '../../images/Carousel/laptop-slider-banner-rtx-3060-1586px-615px-v2.jpg'
import img2 from '../../images/Carousel/honor-magic-2-banner.jpg'
import img3 from '../../images/Carousel/black-friday-super-sale-facebook-cover-template_106176-1576.jpg'

function SimpleSlider() {
  return (
    <Carousel fade className='carousel'>
       <Carousel.Item className='carousel-item'>
        <img
          className="d-block w-100"
          src={img1}
          alt="First slide"
        />
        <Carousel.Caption className='carousel-caption'>
          <h3>First slide label</h3>
          <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img
          className="d-block w-100"
          src={img2}
          alt="First slide"
        />
        <Carousel.Caption>
          <h3>First slide label</h3>
          <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img
          className="d-block w-100"
          src={img3}
          alt="First slide"
        />
        <Carousel.Caption>
          <h3>First slide label</h3>
          <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
        </Carousel.Caption>
      </Carousel.Item>
    </Carousel>
  );
}

export default SimpleSlider;