import React from 'react'
import SimpleSlider from '../../components/SimpleSlider/SimpleSlider'
import Product from '../../components/Products/Product'
import Features from '../../components/Features/Features'
import BestSellers from '../../components/BestSellers/BestSellers'

const Index = () => {
  return (
    <div>
        <SimpleSlider/>
        <Features/>
        <Product/>
        <BestSellers/>
    </div>
  )
}

export default Index