import React, { useEffect } from 'react'
import './pDetails.css'
import { useParams } from 'react-router-dom'
import { useState } from 'react'
import axios from 'axios'
import Spinner from 'react-bootstrap/Spinner';

const PDetails = () => {

  const {id} = useParams()
  const [product,setProduct] = useState()

  useEffect(() => {
    const getProductById = async () => {
      try {
        const response = await axios.get(`https://fakestoreapi.com/products/${id}`);
        setProduct(response.data);
      } catch (error) {
        console.error("Error fetching product:", error);
      }
    };

    getProductById();
  }, [id]); 

  if (!product) {
    return <div  className='d-flex justify-content-center'><Spinner/></div>; 
  }

  return (
    <div class="container">

    <h1 class="my-4">Product
      <small>  Details Page</small>
    </h1>
  
    <div class="row">
  
      <div class="col-md-8">
        <img   style={{ width: "500px", height: "550px" }} class="img-fluid" src={product.image} alt=""/>
      </div>
  
      <div class="col-md-4">
        <h3 class="my-3">{product.title}</h3>
        <p>{product.description}</p>
        <h3 class="my-3">Project Details</h3>
        <ul>
          <li>Category : {product.category}</li>
          <li>Price : ${product.price}</li>
          <li>Rating: {product.rating.rate}</li>
          <li>caount: {product.rating.count}</li>
        </ul>
      </div>
  
    </div>
  </div>
  )
}

export default PDetails