import React, { useContext } from 'react';
import './carddetails.css'
import { ProductContext } from '../../context/ProductContext';

const CardDetails = () => {
  const context = useContext(ProductContext);
  
  const handleRemoveFromCart = (itemId) => {
    context.removeFromCart(itemId);
  };
  
  const handleQuantityChange = (e, itemId) => {
    const newQuantity = parseInt(e.target.value);
    context.updateQuantity(itemId, newQuantity);
  };
  
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
            <th scope="col">Subtotal</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          {context.product.length > 0 ? context.product.map(item => (
            <tr key={item.id} className='table-tr'>
              <td><button onClick={() => handleRemoveFromCart(item.id)}>Remove</button></td>
              <td><img src={item.Image} alt={item.Title} style={{ width: '92px', height: '92px' }} /></td>
              <td>{item.Title}</td>
              <td>${item.Price}</td>
              <td><input type="number" value={item.Quantity} onChange={(e) => handleQuantityChange(e, item.id)} /></td>
              <td>${item.Price * (item.Quantity || 0)}</td> {/* Use 0 if Quantity is null or undefined */}
            </tr>
          )) : <tr><td colSpan="6">Your cart is empty</td></tr>}
        </tbody>
      </table>
    </div>
  );
};

export default CardDetails;
