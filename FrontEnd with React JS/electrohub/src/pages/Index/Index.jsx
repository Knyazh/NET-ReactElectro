import React from 'react'
import SimpleSlider from '../../components/SimpleSlider/SimpleSlider'
import Product from '../../components/Products/Product'
import Features from '../../components/Features/Features'
import BestSellers from '../../components/BestSellers/BestSellers'
import RecommendedForYou from '../../components/Recommended/RecommendedForYou'

const Index = () => {
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