import React from 'react';
import './css/App.css';
import Button from './Button.js';
import Field from './Field.js';

class Calculator extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isFirstNumber: true,
            firstNumber: null,
            operator: null,
            secondNumber: null,
            display: 0
        };
    }

    handleClickNumber(i) {
        this.state.isFirstNumber ? this.setState({ firstNumber: i }) : this.setState({ secondNumber: i });
        this.setState({ isFirstNumber: !this.state.isFirstNumber });
        this.setState({ display: i });
    }

    handleClickOperator(i) {
        if (!this.state.isFirstNumber) { // eslint-disable-next-line
            switch (i) {
                case '+':
                    this.setState({ operator: 'add' });
                    break;
                case '-':
                    this.setState({ operator: 'subtract' });
                    break;
                case '*':
                    this.setState({ operator: 'multiply' });
                    break;
            }

            this.setState({ display: i });
        }
    }

    handleClickEquals(i) {
        if (this.state.firstNumber !== null && this.state.operator !== null && this.state.secondNumber !== null) {
            this.callAPI(this.state.firstNumber, this.state.operator, this.state.secondNumber);
            this.resetCalculator();
        }
    }

    handleClickClear() {
        this.resetCalculator();
        this.setState({ display: 0 });
    }

    resetCalculator() {
        this.setState({ isFirstNumber: true });
        this.setState({ firstNumber: null });
        this.setState({ operator: null });
        this.setState({ secondNumber: null });
    }

    callAPI(firstNumber, operator, secondNumber) {
        fetch(`http://localhost:8080/${operator}/${firstNumber}/${secondNumber}`, {
            method: 'get',
            headers: new Headers({
                'Accept': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            })
        })
            .then(response => response.json())
            .then(display => this.setState({ display: display['number'] }));
    }

    renderField(i) {
        return (
            <Field display={i} />
        );
    }

    renderButtonNumber(i) {
        return (
            <Button
                value={i}
                onClick={() => this.handleClickNumber(i)}
            />
        );
    }

    renderButtonOperator(i) {
        return (
            <Button
                value={i}
                onClick={() => this.handleClickOperator(i)}
            />
        );
    }

    renderButtonEquals(i) {
        return (
            <Button
                value={i}
                onClick={() => this.handleClickEquals(i)}
            />
        );
    }

    renderButtonClear(i) {
        return (
            <Button
                value={i}
                onClick={() => this.handleClickClear(i)}
            />
        );
    }

    render() {
        return (
            <div>
                {this.renderField(this.state.display)}
                <div className="button-wrap">
                    {this.renderButtonOperator('+')}
                    {this.renderButtonNumber(1)}
                    {this.renderButtonNumber(2)}
                    {this.renderButtonNumber(3)}
                    {this.renderButtonOperator('-')}
                    {this.renderButtonNumber(4)}
                    {this.renderButtonNumber(5)}
                    {this.renderButtonNumber(6)}
                    {this.renderButtonOperator('*')}
                    {this.renderButtonNumber(7)}
                    {this.renderButtonNumber(8)}
                    {this.renderButtonNumber(9)}
                    {this.renderButtonEquals('=')}
                    {this.renderButtonClear('CE')}
                    {this.renderButtonNumber(0)}
                </div>
            </div>
        );
    }
}

export default Calculator;