export function SlotItem({slotList}){
    return(
        <div className="slots">
            {slotList.map((item) => (
				<Button className="slot-button"> Slot </Button>
			))}
        </div>
    )
}
