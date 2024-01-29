import React, { useContext } from "react";
import "./carddetails.css";
import { ShopContext } from "../../context/shop-context";

const CardDetails = (props) => {
  const { id, title, price, image } = props.data;
  const { cartItems, addToCart, removeFromCart, updateCartItemCount } =
    useContext(ShopContext);

  return (
    <tr className="table-tr">
      <td>
        <button type="button" className="btn btn-danger"  onClick={() => removeFromCart(id)} >x</button>
      </td>
      <td>
        <img src={image} alt="ssa" style={{ width: "92px", height: "92px" }} />
      </td>
      <td>{title}</td>
      <td>${price}</td>
      <td>
        <input
          value={cartItems[id]}
          onChange={(e) => updateCartItemCount(Number(e.target.value), id)}
        />
      </td>
      <td>
        <button type="button" className="btn btn-primary" onClick={() => addToCart(id)}> + </button>
      </td>
    </tr>
  );
};

export default CardDetails;
