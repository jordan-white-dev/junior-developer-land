import React, { Component } from 'react';
import banner from '../img/banner-1.png';
import '../css/App.css';

export class HotDogHeader extends Component {
  static displayName = HotDogHeader.name;

  render() {
    return (
      <div className="hot-dog-header">
        <img
          src={banner} alt={'An abundance of hotdogs!'}
          className="hot-dog-header-image"
        />
        <div
          className="hot-dog-header-hashtag"
        >
          <p>
            <i
              className="fab fa-instagram"
              id="hot-dog-header-instagram-icon"
            >
            </i>
            #hotdogs
          </p>
        </div>

      </div>
    );
  }
}