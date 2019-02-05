private void setUpdatedOrder()
		{
			if (objOrder != null)
			{
				objOrder.ORDER_ID = int.Parse(this.lblOrder_ID.Text);
				objOrder.STATUS = OrderStatus.NEWORDER;
				objOrder.ORDER_DATE = DateTime.Now.Date;

				//temporary set start time kay wala pa interface sa telephony
				//startTime = string.Format("{0:hh:mm:ss}",DateTime.Now);
				objOrder.ASSIGNED_TIME = string.Format("{0:yyyy-dd-MM hh:mm:ss}", DateTime.Now);
				lineNumber = this.vsfOrderItem.Rows - 1;

				if (!fromMenu)
					objOrder.ORDER_DETAILS.Clear();

				for (int r = 1; r < this.vsfOrderItem.Rows; r++)
				{
					if (this.vsfOrderItem.get_TextMatrix(r, 1) != "")
					{
						OrderDetail OrdDet = new OrderDetail();
						OrdDet.ORDER_ID = this.objOrder.ORDER_ID;
						OrdDet.LINE_NO = this.vsfOrderItem.get_TextMatrix(r, 1);
						Jinisys.POS.Configuration.BusinessLayer.Item item = new Jinisys.POS.Configuration.BusinessLayer.Item();
						item = oItemFacade.GetItem(this.vsfOrderItem.get_TextMatrix(r, 3));
						OrdDet.ITEM = item;
						OrdDet.QUANTITY = int.Parse(this.vsfOrderItem.get_TextMatrix(r, 2));
						if (item == null)
						{
							OrdDet.AMOUNT = 0;
							OrdDet.VAT = 0;
						}
						else
						{
							OrdDet.AMOUNT = decimal.Parse(this.vsfOrderItem.get_TextMatrix(r, 7));
							OrdDet.VAT = decimal.Parse(this.vsfOrderItem.get_TextMatrix(r, 6));
						}
						objOrder.ORDER_DETAILS.Add(OrdDet);

					}
				}

				objOrder.CUSTOMER_ID = this.lblGuestName.Text;
				//temporary values for simulation
				//	objOrder.ROUTE_ID="SAMPLE";

				objOrder.TOTAL_LINE_ITEMS = totalQty;
				objOrder.TOTAL_AMOUNT = vatSales;

				objOrder.VAT_AMOUNT = decimal.Parse(lblVatAmount.Text);
				objOrder.VAT_SALES = decimal.Parse(lblVatSales.Text);
				objOrder.NONVAT_SALES = decimal.Parse(lblNonVatSales.Text);
				objOrder.SERVICE_CHARGE = decimal.Parse(lblServiceCharge.Text);
				objOrder.ROOMSERVICE_CHARGE = 0;

			}
			else
				showMessage("Error : > ", "No New Order has been created!");

		}
