import { createContext, useEffect, useState } from "react";

export const ProductContext = createContext()

export const ProductProvider =  ({children})=>{

    const storedProduct = JSON.parse(localStorage.getItem('products')) || [];
    const [product, setProduct] = useState(storedProduct)

    useEffect(()=>{
        localStorage.setItem('products',JSON.stringify(product))
    }, [product])

    const addToCart = (newProduct) =>{

        setProduct(prevData => [...prevData, newProduct])
    }

    const removeFromCart = (productId) =>{

        setProduct(prevData => prevData.filter(item => item.id !== productId))

    }

    useEffect(()=>{
        if(JSON.parse(localStorage.getItem('products'))===null){
            localStorage.setItem('products', JSON.stringify([]))
        }
    }, [])

return(
    <ProductContext.Provider value={{product,addToCart,removeFromCart}}>
        {children}
    </ProductContext.Provider>
)

}