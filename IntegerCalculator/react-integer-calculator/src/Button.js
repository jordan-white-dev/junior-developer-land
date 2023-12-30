import React from 'react';

function Button(props) {
    return (
        <div
            className="button-appearance"
            onClick={props.onClick}
        >
            {props.value}
        </div>
    );
}

export default Button;