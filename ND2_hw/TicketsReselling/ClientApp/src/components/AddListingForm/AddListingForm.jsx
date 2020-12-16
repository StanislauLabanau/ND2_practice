import React from "react";
import { Field, reduxForm } from "redux-form";

const AddListingForm = (props) => {
    const { handleSubmit } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="price">Price</label>
                <Field
                    name="price"
                    component={Input}
                    type="number"
                    validate={[required, moreThanZero]}
                />
            </div>
            <div>
                <label htmlFor="amount">Amount</label>
                <Field
                    name="amount"
                    component={Input}
                    type="number"
                    validate={[required, moreThanZero, checkInteger]}
                />
            </div>
            <div>
                <label htmlFor="note">Note</label>
                <Field
                    name="note"
                    component={Input}
                    type="text"
                />
            </div>
            <button type="submit">Submit</button>
        </form>
    );
};

const FormControl = ({ input, meta, ...rest }) => {
    const hasError = meta.touched && meta.error;
    return (
        <div>
            <div>{rest.children}</div>
            {hasError && <span>{meta.error}</span>}
        </div>
    );
};

export const Input = (props) => {
    const { input, meta, child, ...rest } = props;
    return (
        <FormControl {...props}>
            <input {...input} {...rest} />
        </FormControl>
    );
};

const required = (value) => {
    if (value) return undefined;

    return "Field is required";
};

const checkInteger = value => {
    if (Number.isInteger(Number(value))) return undefined;

    return "Value must be integer";
}

const moreThanZero = value => {
    if (Number(value) > 0) return undefined;

    return "Value must be more than 0";
}

export default reduxForm({ form: "contact" })(AddListingForm);
