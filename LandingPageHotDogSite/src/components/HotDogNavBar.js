import React, { Component } from 'react';
import '../css/App.css';

export class HotDogNavBar extends Component {
  static displayName = HotDogNavBar.name;

  render() {
    return (
      <div id="hot-dog-nav-bar">
        <div class="hot-dog-nav-bar-right">MENU</div>
        <div class="hot-dog-nav-bar-right">CATERING</div>
        <div class="hot-dog-nav-bar-right">ABOUT US</div>
        <div>CONTACT</div>
      </div>
    );
  }
}