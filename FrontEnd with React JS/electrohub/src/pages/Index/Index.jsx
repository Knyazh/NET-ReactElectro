import React, { useEffect, useState } from 'react'
import SimpleSlider from '../../components/SimpleSlider/SimpleSlider'
import Product from '../../components/Products/Product'
import Features from '../../components/Features/Features'
import BestSellers from '../../components/BestSellers/BestSellers'
import RecommendedForYou from '../../components/Recommended/RecommendedForYou'
import axios from 'axios'

const Index = () => {
  const [products,setProducts]= useState([])

  useEffect(()=>{
  
    const getProducts = async () => {
      await axios.get('https://fakestoreapi.com/products?limit=5')
      .then(response => setProducts(response.data))
      .catch(err => console.log(err))
    }
  
    getProducts()
  
  },[])

  return (
    <div>
        <SimpleSlider/>
        <Features/>
        <Product/>
        <BestSellers/>
        <RecommendedForYou/>
    </div>
  )
}

export default Index