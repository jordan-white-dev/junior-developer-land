import React from 'react';
import '../css/App.css';

function HotDogExampleImageLeft(props) {
    return (
        <div className="hot-dog-example">
            <div className="hot-dog-example-image">
                <img
                    className="hot-dog-example-image-left"
                    src={props.imagePath}
                    alt={props.imageAltText}
                />
            </div>
            <div className="hot-dog-example-text">
                <p className="hot-dog-example-title">⁠— &nbsp; {props.title}<br /></p>
                <p className="hot-dog-example-text-contents">
                    {props.loremIpsumText}
                </p>
                <p>
                    <br />
                </p>
            </div>
        </div>
    );
}

export default HotDogExampleImageLeft;