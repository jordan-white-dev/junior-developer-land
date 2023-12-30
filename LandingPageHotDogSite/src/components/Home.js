import React from 'react';
import { HotDogHeader } from './HotDogHeader.js';
import { HotDogNavBar } from './HotDogNavBar.js';
import { HotDogBlurb } from './HotDogBlurb';
import HotDogExampleImageLeft from './HotDogExampleImageLeft.js';
import HotDogExampleImageRight from './HotDogExampleImageRight.js';
import { HotDogFooter } from './HotDogFooter.js';
import '../css/App.css';
import beefImage from '../img/beef-1.png';
import veganImage from '../img/vegan-1.png';
import vegetarianImage from '../img/vegetarian-1.png';

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loremIpsumText: 'Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.',
            exampleOneTitle: 'Gourmet All Beef Hotdogs',
            exampleOneAltText: 'A delicious all-beef hotdog!',
            exampleTwoTitle: '⁠Vegan Hotdogs',
            exampleTwoAltText: 'A delicious vegan hotdog!',
            exampleThreeTitle: '⁠Vegetarian Hotdogs',
            exampleThreeAltText: 'A delicious vegetarian hotdog!',
        };
    }

    renderHotDogExampleImageLeft(i, j, k, m) {
        return (
            <HotDogExampleImageLeft
                title={i}
                loremIpsumText={j}
                imagePath={k}
                imageAltText={m}
            />
        );
    }

    renderHotDogExampleImageRight(i, j, k, m) {
        return (
            <HotDogExampleImageRight
                title={i}
                loremIpsumText={j}
                imagePath={k}
                imageAltText={m}
            />
        );
    }

    render() {
        return (
            <div>
                <HotDogHeader />
                <HotDogNavBar />
                <HotDogBlurb />
                {this.renderHotDogExampleImageRight(this.state.exampleOneTitle, this.state.loremIpsumText, beefImage, this.state.exampleOneAltText)}
                {this.renderHotDogExampleImageLeft(this.state.exampleTwoTitle, this.state.loremIpsumText, veganImage, this.state.exampleTwoAltText)}
                {this.renderHotDogExampleImageRight(this.state.exampleThreeTitle, this.state.loremIpsumText, vegetarianImage, this.state.exampleThreeAltText)}
                <HotDogFooter />
            </div>
        );
    }
}

export default Home;