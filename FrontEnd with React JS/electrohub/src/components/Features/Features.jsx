import React from 'react'
import './features.css'
import { TbTruckDelivery } from "react-icons/tb";
import { FaHandHoldingUsd } from "react-icons/fa";
import { TbRotate360 } from "react-icons/tb";
import { SiContactlesspayment } from "react-icons/si";
import { CiShoppingTag } from "react-icons/ci";




const Features = () => {
  return (
    <div className=' feature-list  row d-flex justify-content-center'>
            <div className='features '>
               <div className="media d-flex justify-content-center">
                <div className='logo'><TbTruckDelivery className='delivery-logo' /></div>
                <div>
                    <strong>Free Delivery</strong> <br />
                    from 50$
                </div>
               </div>

            </div>
            <div className='features '>
               <div className="media d-flex justify-content-center">
                <div className='logo'><TbRotate360 className='delivery-logo' /></div>
                <div>
                    <strong>365 days</strong> <br />
                    For free return
                </div>
               </div>

            </div>
            <div className='features '>
               <div className="media d-flex justify-content-center">
                <div className='logo'><FaHandHoldingUsd className='delivery-logo' /></div>
                <div>
                    <strong>99% Positive</strong> <br />
                    Feedbacks
                </div>
               </div>

            </div>
            <div className='features '>
               <div className="media d-flex justify-content-center">
                <div className='logo'><SiContactlesspayment className='delivery-logo' /></div>
                <div>
                    <strong>Payment</strong> <br />
                    Secure System
                </div>
               </div>

            </div>
            <div className='features '>
               <div className="media d-flex justify-content-center">
                <div className='logo'><CiShoppingTag className='delivery-logo' /></div>
                <div>
                    <strong>Only Best</strong> <br />
                   Brands
                </div>
               </div>

            </div>
            
    </div>
  )
}

export default Features