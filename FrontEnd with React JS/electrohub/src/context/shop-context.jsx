import { createContext, useEffect, useState } from "react";
import axios from "axios";

export const ShopContext = createContext(null);



export const ShopContextProvider = (props) => {
   const [products, setProducts] = useState([]);

  useEffect(() => {
    
 const getProducts = async () => {
   await axios
     .get("https://fakestoreapi.com/products?limit=6")
     .then((response) => setProducts(response.data))
     .catch((err) => console.log(err));
 };

 getProducts();
}, []);

const getDefaultCart = () => {
 let cart = {};
 for (let i = 1; i < products.length + 1; i++) {
   cart[i] = 0;
 }
 return cart;
};

  const [cartItems, setCartItems] = useState(getDefaultCart());

  const getTotalCartAmount = () => {
    let totalAmount = 0;
    for (const item in cartItems) {
      if (cartItems[item] > 0) {
        let itemInfo = products.find((product) => product.id === Number(item));
        totalAmount += cartItems[item] * itemInfo.price;
      }
    }
    return totalAmount;
  };

  const addToCart = (itemId) => {
    setCartItems((prev) => ({ ...prev, [itemId]: prev[itemId] + 1 }));
  };

  const removeFromCart = (itemId) => {
    setCartItems((prev) => ({ ...prev, [itemId]: prev[itemId] - 1 }));
  };

  const updateCartItemCount = (newAmount, itemId) => {
    setCartItems((prev) => ({ ...prev, [itemId]: newAmount }));
  };

  const checkout = () => {
    setCartItems(getDefaultCart());
  };

  const contextValue = {
    cartItems,
    products,
    addToCart,
    updateCartItemCount,
    removeFromCart,
    getTotalCartAmount,
    checkout,
  };

  return (
    <ShopContext.Provider value={contextValue}>
      {props.children}
    </ShopContext.Provider>
  );
};
