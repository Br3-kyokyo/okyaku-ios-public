class TriggerSentencesController < ApplicationController
  before_action :set_trigger_sentence, only: [:show, :edit, :update, :destroy]

  # GET /trigger_sentences
  # GET /trigger_sentences.json
  def index
    @trigger_sentences = TriggerSentence.all
  end

  # GET /trigger_sentences/1
  # GET /trigger_sentences/1.json
  def show
  end

  # GET /trigger_sentences/new
  def new
    @trigger_sentence = TriggerSentence.new
  end

  # GET /trigger_sentences/1/edit
  def edit
  end

  # POST /trigger_sentences
  # POST /trigger_sentences.json
  def create
    @trigger_sentence = TriggerSentence.new(trigger_sentence_params)

    respond_to do |format|
      if @trigger_sentence.save
        format.html { redirect_to @trigger_sentence, notice: 'Trigger sentence was successfully created.' }
        format.json { render :show, status: :created, location: @trigger_sentence }
      else
        format.html { render :new }
        format.json { render json: @trigger_sentence.errors, status: :unprocessable_entity }
      end
    end
  end

  # PATCH/PUT /trigger_sentences/1
  # PATCH/PUT /trigger_sentences/1.json
  def update
    respond_to do |format|
      if @trigger_sentence.update(trigger_sentence_params)
        format.html { redirect_to @trigger_sentence, notice: 'Trigger sentence was successfully updated.' }
        format.json { render :show, status: :ok, location: @trigger_sentence }
      else
        format.html { render :edit }
        format.json { render json: @trigger_sentence.errors, status: :unprocessable_entity }
      end
    end
  end

  # DELETE /trigger_sentences/1
  # DELETE /trigger_sentences/1.json
  def destroy
    @trigger_sentence.destroy
    respond_to do |format|
      format.html { redirect_to trigger_sentences_url, notice: 'Trigger sentence was successfully destroyed.' }
      format.json { head :no_content }
    end
  end

  private
    # Use callbacks to share common setup or constraints between actions.
    def set_trigger_sentence
      @trigger_sentence = TriggerSentence.find(params[:id])
    end

    # Never trust parameters from the scary internet, only allow the white list through.
    def trigger_sentence_params
      params.require(:trigger_sentence).permit(:body_en, :body_ja, :trigger_id, :position)
    end
end
