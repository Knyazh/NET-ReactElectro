import React, { useContext } from 'react'
import './cartdetails.css'
import { ShopContext } from "../../context/shop-context";
import CardDetails from '../../components/CardDetails/CardDetails'
import { useNavigate } from 'react-router-dom';

const CartDetails = () => {

  const { cartItems, getTotalCartAmount, checkout, products } = useContext(ShopContext);
  const totalAmount = getTotalCartAmount();

  const navigate = useNavigate();

  return (
    <div>
      <table className="table">
        <thead>
          <tr>
            <th scope="col"><span>Remove from Cart</span></th>
            <th scope="col"><span>Thumbnail</span></th>
            <th scope="col">Product</th>
            <th scope="col">Price</th>
            <th scope="col">Quantity</th>
            <th scope="col">Add to cart</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
         {
          products.map((product)=>{
            if(cartItems[product.id] !== 0 ){
              return   <CardDetails data={product}/>
            }
            return null;
          })
         }
        
        
        </tbody>
      </table>

      {totalAmount > 0 ? (
        <div className="checkout">
          <p> Subtotal: ${totalAmount} </p>
          <button onClick={() => navigate("/")}> Continue Shopping </button>
          <button
            onClick={() => {
              checkout();
              navigate("/checkout");
            }}
          >
            {" "}
            Checkout{" "}
          </button>
        </div >
      ) : (
        <h1 className='shopping-cart-empty'> Your Shopping Cart is Empty</h1>
      )}
    </div>
  )
}

export default CartDetails