import React, { Component } from 'react';
import '../css/App.css';

export class HotDogFooter extends Component {
  static displayName = HotDogFooter.name;

  render() {
    return (
      <div className="hot-dog-footer">
        <div className="hot-dog-footer-copyright">
          <p>® Dirty Dogs all rights reserved</p>
        </div>
        <div className="hot-dog-footer-info">
          <p>274 Marconi Blvd. Columbus, Ohio 43215 &nbsp; | &nbsp; 614.538.0095 &nbsp; | &nbsp; Contact Us</p>
        </div>
      </div>
    );
  }
}