import React, { Component } from 'react';
import image from '../img/hot-dog.png';
import '../css/App.css';

export class HotDogBlurb extends Component {
  static displayName = HotDogBlurb.name;

  render() {
    return (
      <div className="hot-dog-blurb">
        <img
          className="hot-dog-blurb-image"
          src={image}
          alt={'transparent hot dog'}
        />
        <p
          className="hot-dog-blurb-text"
        >
          Dirty Dogs serves all-beef, vegan and vegetarian hot dogs.
        </p>
        <div
          className="hot-dog-blurb-button"
        >
          More Dogs'n Make Em Hot
        </div>
      </div>
    );
  }
}