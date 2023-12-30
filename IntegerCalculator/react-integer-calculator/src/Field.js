import React from 'react';

function Field(props) {
    return (
        <div
            className="field-appearance"
        >
            {props.display}
        </div>
    );
}

export default Field;