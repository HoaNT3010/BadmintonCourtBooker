export const CreateForm = () => {
    const dispatch = useDispatch();
    const list = useSelector((state) => state.flowers.flowers);
    const nextID = list[list.length-1].id;
  
    // useEffect(() => {
    //   fetch('https://665e9fe91e9017dc16f0af1c.mockapi.io/api/v1/flower/')
    //   .then(response => response.json())
    //   .then(data => setNextID(data[data.length-1].id))
    //   .catch(error => console.log(error.message));
    // })
  
    const navigate = useNavigate();
    // console.log(nextID);
    const validate = (values) => {
      const errors = {};
      if (!values.name) {
        errors.name = "Required";
      } else if (values.name.length > 100) {
        errors.name = "Must be 100 characters or less";
      }
  
      if (!values.description) {
        errors.description = "Required";
      } else if (values.description.length > 1000) {
        errors.description = "Must be 1000 characters or less";
      }
  
      if (!values.price) {
        errors.price = "Required";
      } else if (values.price < 0) {
        errors.price = "Invalid price";
      }
  
      if (!values.stockCount) {
        errors.stockCount = "Required";
      } else if (values.stockCount < 0) {
        errors.stockCount = "Invalid stock amount";
      }
  
      // if (!values.picture) {
      //   errors.picture = "Required";
      // } else if (
      //   !/^(https?:\/\/)?([\da-z\.-]+)\.([a-z]{2,6})([\/\w\.-]*)*\/?$/i.test(values.picture)
      // ) {
      //   errors.picture = "Invalid photo URL";
      // }
      return errors;
    };
  
    const formik = useFormik({
      initialValues: {
        name: "",
        origin: "",
        description: "",
        price: 0,
        stockCount: 0,
        picture: "https://media.istockphoto.com/id/1396814518/vector/image-coming-soon-no-photo-no-thumbnail-image-available-vector-illustration.jpg?s=612x612&w=0&k=20&c=hnh2OZgQGhf0b46-J2z7aHbIWwq8HNlSDaNp2wn_iko=",
        id: nextID,
      },
      validate,
      onSubmit: (values, { setSubmitting }) => {
        setTimeout(() => {
          dispatch(
            addFlowerAsync({
              id: values.id,
              name: values.name,
              description: values.description,
              origin: values.origin,
              price: values.price,
              stockCount: values.stockCount,
              picture: values.picture,
            })
          );
          setSubmitting(false);
        }, 1000);
        toast.success("Add done",{
                  position: "top-right",
                  onClose: () => navigate('/admin'),
                  closeOnClick: true,
                  autoClose: 3000
              });
      },
    });
  
    return (
      <>
      <h2>Create Flower</h2>
      <div className="form-container">
        <div className="form-img">
          <img src={formik.values.picture}></img>
        </div>
        <form className="form-item" onSubmit={formik.handleSubmit}>
          <label htmlFor="name">Flower Name</label>
          <input
            className="item-input"
            id="name"
            name="name"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.name}
          />
          {formik.errors.name ? <div className="form-warning">{formik.errors.name}</div> : null}
  
          <label htmlFor="description">Description</label>
          <input
            className="item-input"
            id="description"
            name="description"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.description}
          />
          {formik.errors.description ? (
            <div className="form-warning">{formik.errors.description}</div>
          ) : null}
          <label htmlFor="origin">Origin</label>
          <input
            className="item-input"
            id="origin"
            name="origin"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.origin}
          />
          {formik.errors.origin ? <div className="form-warning">{formik.errors.origin}</div> : null}
          <label htmlFor="price">Price</label>
          <input
            className="item-input"
            id="price"
            name="price"
            type="number"
            onChange={formik.handleChange}
            value={formik.values.price}
          />
          {formik.errors.price ? <div className="form-warning">{formik.errors.price}</div> : null}
          <label htmlFor="stockCount">Stock Amount</label>
          <input
            className="item-input"
            id="stockCount"
            name="stockCount"
            type="number"
            onChange={formik.handleChange}
            value={formik.values.stockCount}
          />
          {formik.errors.stockCount ? (
            <div className="form-warning">{formik.errors.stockCount}</div>
          ) : null}
          <label htmlFor="picture">Photo Link</label>
          <input
            className="item-input"
            id="picture"
            name="picture"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.picture}
          />
          {formik.errors.picture ? <div className="form-warning">{formik.errors.picture}</div> : null}
          <button type="submit">Submit</button>
        </form>
        <ToastContainer />
      </div>
      </>
      
    );
  };
  