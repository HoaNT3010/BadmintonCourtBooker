export const UpdateForm = () => {
    const objName = useParams();
    const dispatch = useDispatch();
    const objs = useSelector((state) => state.flowers.flowers);
    const object = objs.find((obj) => {
      return obj.id == objName.id;
    });
    //   const [description, setDesc] = useState('');
    //   const [origin, setOrigin] = useState('');
    //   const [price, setPrice] = useState(0);
    //   const [stockCount, setStock] = useState(0);
    //   const [picture, setPicture] = useState('');
    
  
  
    const navigate = useNavigate();
    // useEffect(() => {
    //   dispatch(getFlowersAsync());
    // }, [dispatch]);
  //   useEffect(() => {
  //     fetch("https://665e9fe91e9017dc16f0af1c.mockapi.io/api/v1/flower/")
  //       .then((response) => response.json())
  //       .then((data) => setAPI(data.find()))
  //       .catch((error) => console.log(error.message));
  //   });
  
  
  
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
      //   !/^(https?:\/\/)?([\da-z\.-]+)\.([a-z]{2,6})([\/\w\.-]*)*\/?$/i.test(
      //     values.picture
      //   )
      // ) {
      //   errors.picture = "Invalid photo URL";
      // }
      return errors;
    };
  
    const formik = useFormik({
      initialValues: {
        name: object.name,
        origin: object.origin,
        description: object.description,
        price: object.price,
        stockCount: object.stockCount,
        picture: object.picture,
        id: object.id,
      },
      validate,
      onSubmit: (values, { setSubmitting }) => {
        setTimeout(() => {
          dispatch(
            updateFlowerAsync({
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
        toast.success("Update done",{
                  position: "top-right",
                  onClose: () => navigate('/admin'),
                  closeOnClick: true,
                  autoClose: 3000
              });
      },
    });
  
    return (
      <>
      <h2>Update Flower</h2>
      <div className="form-container">
        <div className="form-img">
          <img src={formik.values.picture}></img>
        </div>
        
        <form className="form-item" onSubmit={formik.handleSubmit}>
          <label htmlFor="name">Flower Name</label>
          <input className="item-input"
            id="name"
            name="name"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.name}
          />
          {formik.errors.name ? <div className="form-warning">{formik.errors.name}</div> : null}
  
          <label htmlFor="description">Description</label>
          <input className="item-input"
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
          <input className="item-input"
            id="origin"
            name="origin"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.origin}
          />
          {formik.errors.origin ? <div className="form-warning">{formik.errors.origin}</div> : null}
          <label htmlFor="price">Price</label>
          <input className="item-input"
            id="price"
            name="price"
            type="number"
            onChange={formik.handleChange}
            value={formik.values.price}
          />
          {formik.errors.price ? <div className="form-warning">{formik.errors.price}</div> : null}
          <label htmlFor="stockCount">Stock Amount</label>
          <input className="item-input"
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
          <input className="item-input"
            id="picture"
            name="picture"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.picture}
          />
          {formik.errors.picture ? <div className="form-warning">{formik.errors.picture}</div> : null}
          <button type="submit">Submit</button>
        </form>
        
      </div>
      <ToastContainer/>
      </>
      
    );
  };