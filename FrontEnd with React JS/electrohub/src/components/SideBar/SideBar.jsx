import React from 'react'
import { Link } from 'react-router-dom' 
import './sidebar.css'

export const SideBar = () => {
  return (
        <div className='sidebar'>
    <aside>
      <span>Browse Categories</span>
       <ul>
        <li>
          <Link></Link>
        </li>
        <li>
          <Link></Link>
        </li>
        <li>
          <Link></Link>
        </li>
        <li>
          <Link></Link>
        </li>
        <li>
          <Link></Link>
        </li>
        <li>
          <Link></Link>
        </li>
       </ul>
    </aside>
    </div>
  )
}
