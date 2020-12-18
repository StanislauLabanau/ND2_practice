import React, { Component } from "react";
import Modal from "react-modal";
import { connect } from "react-redux";
import { closeAddListingModal, sendListing } from "../../redux/catalog-reducer";
import AddListingForm from "../AddListingForm/AddListingForm";

Modal.setAppElement("#root");

const customStyles = {
    content: {
        top: "50%",
        left: "50%",
        right: "auto",
        bottom: "auto",
        marginRight: "-50%",
        transform: "translate(-50%, -50%)",
    },
};

class AddListingModal extends Component {

    handleSubmit = values => {
        this.props.sendListing({ ...values, eventId: this.props.eventId })
    }

    render() {
        const { isModalOpened, closeAddListingModal } = this.props;

        return (
            <div>
                <Modal
                    isOpen={isModalOpened}
                    style={customStyles}
                    shouldCloseOnOverlayClick
                >
                    <h2>Provide listing details</h2>
                    <AddListingForm onSubmit={this.handleSubmit} />
                    <button onClick={closeAddListingModal}>Close</button>
                </Modal>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
        isModalOpened: state.catalog.addListing.isAddListingModalOpened,
        eventId: state.catalog.addListing.eventId
    };
}

export default connect(mapStateToProps, { closeAddListingModal, sendListing })(AddListingModal);